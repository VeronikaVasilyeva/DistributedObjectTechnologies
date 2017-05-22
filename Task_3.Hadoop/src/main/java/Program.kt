//hadoop jar hadoop-task.jar /user/s0196/rus_web_2002_300K-sentences.txt /user/s0196/result4/ > result.txt
//hdfs dfs -cat /example/data/wordcountout/*
//hdfs dfs -get output local_results

import org.apache.hadoop.conf.Configuration
import org.apache.hadoop.fs.Path
import org.apache.hadoop.io.LongWritable
import org.apache.hadoop.io.Text
import org.apache.hadoop.mapreduce.Job
import org.apache.hadoop.mapreduce.Mapper
import org.apache.hadoop.mapreduce.Reducer
import org.apache.hadoop.mapreduce.lib.input.FileInputFormat
import org.apache.hadoop.mapreduce.lib.input.TextInputFormat
import org.apache.hadoop.mapreduce.lib.output.FileOutputFormat
import org.apache.hadoop.mapreduce.lib.output.TextOutputFormat
import java.util.*

fun main(args: Array<String>) {
    val program = Program
    program.start(args)
}

object Program {
    fun start(args: Array<String>) {
        val conf = Configuration()

        if (args.size != 2) {
            System.err.println("Error arguments" + args.toString())
            System.exit(2)
        }

        val job = Job.getInstance(conf, "n-gram count")
        job.setJarByClass(Program::class.java)

        job.mapperClass = NGramMapper::class.java
        job.reducerClass = NGramReducer::class.java

        job.inputFormatClass = TextInputFormat::class.java
        job.outputFormatClass = TextOutputFormat::class.java

        job.outputKeyClass = Text::class.java
        job.outputValueClass = Text::class.java

        FileInputFormat.addInputPath(job, Path(args[0]))
        FileOutputFormat.setOutputPath(job, Path(args[1]));

        job.waitForCompletion(true)
    }

    class NGramMapper : Mapper<LongWritable, Text, Text, Text>() {

        public override fun map(keyIn: LongWritable, valueIn: Text, context: Mapper<LongWritable, Text, Text, Text>.Context) {
            val str = valueIn.toString().split(Regex("^\\d+\\s+"))[1]
            var words = str.split(Regex("[^a-zA-ZА-Яа-яё_]+")).toMutableList()
            words.removeAll( predicate = {i -> i.equals("")})

            for (i in 0..words.size - 3) {
                val digram = words[i] + " " + words[i + 1]
                context.write(Text(digram), Text(""))               //digram - key
                context.write(Text(digram), Text(words[i+2]))
            }

            if (words.size >= 2 ) {
                val i = words.size - 2
                val digram = words[i] + " " + words[i + 1]
                context.write(Text(digram), Text(""))
            }
        }
    }

    class NGramReducer : Reducer<Text, Text, Text, Text>() {
        //пары «ключ-значение» сортируются и группируются по ключу
        public override fun reduce(key: Text, values: Iterable<Text>, context: Reducer<Text, Text, Text, Text>.Context) {
            val digram = key.toString()
            var amountThisDigram = 0     //кол-во пустых строк - кол-во диграммы этой в тексте
            var amountTrigramsForThisDigram = HashMap<String, Int>()

            for (item in values) {                      //переберем все третьи слова
                val thirdWord = item.toString()
                if (thirdWord.equals("")) amountThisDigram += 1

                if (!amountTrigramsForThisDigram.containsKey(thirdWord)) {
                    amountTrigramsForThisDigram[thirdWord] = 1
                } else {
                    amountTrigramsForThisDigram[thirdWord] = amountTrigramsForThisDigram.get(thirdWord)!! + 1
                }
            }

            for (thirdWord in amountTrigramsForThisDigram.keys) {
                val keyOut = "$digram $thirdWord "

                val amountThisTrigram = "${amountTrigramsForThisDigram.get(thirdWord)!!}"
                val probability = "$amountThisTrigram".toDouble() / "$amountThisDigram".toDouble()

                val valueOut = "$amountThisTrigram $amountThisDigram $probability"

                context.write(Text("$keyOut"), Text("$valueOut"))
            }
        }
    }
}