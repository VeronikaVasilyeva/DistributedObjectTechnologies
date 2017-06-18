var myApp = angular.module("myApp", [])
    .controller("mainController", function($scope, $http, $interval, service, xs, url) {
        var cpuChart = service.initChart("#cpu-chart");
        var ramChart = service.initChart("#ram-chart");

        var counter = 0;
        var cpuData = service.getDataContainer();
        var ramData = service.getDataContainer();

        $interval(function() {
            $http.get(url)
                .then(function(response) {
                    counter++;

                    //сброс данных
                    if (counter === 51) {
                        cpuData = service.getDataContainer();
                        ramData = service.getDataContainer();
                        counter = 1;
                    }

                    service.updateRows(cpuData, _(response.data).map(function(i) { return { key: i.Key, value: i.Value.Cpu } }), counter);
                    service.updateRows(ramData, _(response.data).map(function(i) { return { key: i.Key, value: i.Value.Ram / 1024 / 1024 / 1024 / 8 * 100 } }), counter);

                    cpuChart.load({ columns: cpuData.concat([xs]) });
                    ramChart.load({ columns: ramData.concat([xs]) });
                });
        }, 2000);
    })
    .service("service", function(xs, dataTemplate) {
        var self = this;

        self.getDataContainer = function() {
            return [self.getDataRow("average")];
        };
        self.getDataRow = function(name) {
            var result = angular.copy(dataTemplate);
            result[0] = name;
            return result;
        };

        self.updateRows = function(container, data, index) {
            var average = 0;

            _(data).each(function(i) {
                if (_(container).every(function(e) { return e[0] !== i.key; })) {
                    container.push(self.getDataRow(i.key));
                }

                _(container).find(function(e) { return e[0] === i.key; })[index] = i.value.toFixed(2);
                average += i.value;
            });

            average /= data.length;
            _(container).find(function(e) { return e[0] === "average"; })[index] = average.toFixed(2);
        };

        self.initChart = function(id) {
            return c3.generate({
                bindto: id,
                data: {
                    xs: {
                        "#1": "xs",
                        "#2": "xs",
                        "#3": "xs",
                        "#4": "xs",
                        "average": "xs"
                    },
                    columns: [xs],
                    types: {
                        "#1": "area-spline",
                        "#2": "area-spline",
                        "#3": "area-spline",
                        "#4": "area-spline",
                        "average": "area-spline"
                    }
                }
            });
        };
    })
    .constant("url", "http://localhost:8080/")
    .constant("xs", [
        "xs",
        1, 2, 3, 4, 5, 6, 7, 8, 9, 10,
        11, 12, 13, 14, 15, 16, 17, 18, 19, 20,
        21, 22, 23, 24, 25, 26, 27, 28, 29, 30,
        31, 32, 33, 34, 35, 36, 37, 38, 39, 40,
        41, 42, 43, 44, 45, 46, 47, 48, 49, 50
    ])
    .constant("dataTemplate", [
        "template",
        null, null, null, null, null, null, null, null, null, null,
        null, null, null, null, null, null, null, null, null, null,
        null, null, null, null, null, null, null, null, null, null,
        null, null, null, null, null, null, null, null, null, null,
        null, null, null, null, null, null, null, null, null, null
    ]);