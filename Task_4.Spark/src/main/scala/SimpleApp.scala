import org.apache.spark.SparkContext
import org.apache.spark.SparkContext._
import org.apache.spark.SparkConf
import org.apache.spark.rdd.RDD

def getIdfs(docsCount: Long, docs: RDD[(String, Array[String])]) : RDD[(String, Double)] = {
  docs.flatMap({ case (docId, words) => words.map((_, docId)) })
    .groupByKey()
    .mapValues( { case docIds => (docsCount - docIds.size + 0.5) / (docIds.size + 0.5)})
}

def getScores(docs: RDD[(String, Array[String])], idfs: scala.collection.Map[String, Double], avgdl: Double) : RDD[(String, Array[(String, Double)])]= {
  docs.flatMap({ case (docId, words) =>
    val wordsCount = words.groupBy(s => s).mapValues(_.length)
    words.distinct.map(word => (word, (docId, (idfs(word) * wordsCount(word) * 2.2) / (wordsCount(word) + 1.2 * (0.5 + 0.5*words.length/avgdl)))))
  })
    .groupByKey()
    .mapValues(_.toArray.sortBy(_._2).reverse)
}

def findTop(query: String, scores: RDD[(String, Array[(String, Double)])]) = {
  query.split("\\P{L}")
    .filterNot(_.isEmpty())
    .flatMap(scores.lookup(_).head.take(10))
    .groupBy(_._1)
    .mapValues(_.map(_._2).sum)
    .toArray
    .sortBy(_._2)
    .reverse
    .map(_._1)
    .take(10)
    .mkString("\n")
}

val conf = new SparkConf().setAppName("SimpleApp")
val sc = new SparkContext(conf)

val texts = sc.wholeTextFiles("opencorpora")

val docs = texts.mapValues(
  _.split("\\P{L}")
    .filterNot(_.isEmpty())
).cache()

val N = docs.count()

val avgdl = docs.map(x => (x._2.length)).reduce(_ + _) / N

val idf = getIdfs(docs.count(), docs).collectAsMap()

val scores = getScores(docs, idf, avgdl).cache()

