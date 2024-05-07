"use strict"

declare var Chart;

class MemberBettingBehavior {
    private $: JQueryStatic;
    $exportExcelForm: JQuery;

    constructor(
        $: JQueryStatic
    ) {
        this.$ = $
    }

    cacheDOMElements(): void {
        this.$exportExcelForm = this.$("#export-excel-form");
    }

    init() {
        this.cacheDOMElements();
        this.bindEvents();

        this.initCharts();
    }

    bindEvents(): void {
        this.$("#icon-excel").on("click", this.exportExcel);
    }

    exportExcel = () => {
        this.$exportExcelForm.submit();
    };

    initCharts() {
        Chart.defaults.plugins.legend.position = 'bottom';
        Chart.defaults.plugins.legend.labels.color = '#333';
        Chart.defaults.plugins.title.color = '#333';
        Chart.defaults.plugins.title.display = true;
        Chart.defaults.plugins.title.font.size = 16;

        this.drawTurnoverBySport();
        this.drawTurnoverByBetType();
        this.drawTurnoverAndWinLossByDate();
    }

    drawTurnoverBySport() {
        const data = {
            labels: ['Soccer', 'Basketball', 'Saba Soccer', 'Allbet', 'Others'],
            datasets: [
                {
                    data: [50, 5, 10, 15, 20],
                    //backgroundColor: ['#36a2eb', '#ff6384', '#ff9f40', '#ffcd56', '#4bc0c0', '#9966ff', '#c9cbcf'],
                    backgroundColor: [
                        'rgb(54, 162, 235)',
                        'rgb(255, 99, 132)',
                        'rgb(255, 159, 64)',
                        'rgb(255, 205, 86)',
                        'rgb(75, 192, 192)',
                        'rgb(153, 102, 255)',
                        'rgb(201, 203, 207)'
                    ]
                }
            ]
        };

        const config = {
            type: 'doughnut',
            data: data,
            options: {
                plugins: {
                    title: {
                        text: 'Turnover By Sport',
                        padding: {
                            bottom: 30
                        }
                    },
                    tooltip: {
                        callbacks: {
                            label: function (context) {
                                return `${context.formattedValue}%`;
                            }
                        }
                    }
                }
            }
            //plugins: [ChartDataLabels, DoughnutLabel]
        };
        new Chart(document.getElementById('doughnutChart'), config);
    }

    drawTurnoverByBetType() {
        const labels = ['Over/Under', 'Handicap', '1H Over/Under', 'Others'];
        const data = {
            labels: labels,
            datasets: [
                {
                    label: 'Live',
                    data: [1000, 200, 0, 100],
                    borderColor: 'rgb(54, 162, 235)',
                    backgroundColor: 'rgb(54, 162, 235, 0.5)'
                },
                {
                    label: 'Non-Live',
                    data: [4000, 1000, 200, 3000],
                    borderColor: 'rgb(255, 159, 64)',
                    backgroundColor: 'rgb(255, 159, 64, 0.5)'
                }
            ]
        };

        const config = {
            type: 'bar',
            data: data,
            options: {
                barThickness: 25,
                indexAxis: 'y',
                elements: {
                    bar: {
                        borderWidth: 1
                    }
                },
                plugins: {
                    title: {
                        text: 'Turnover By Bet Type'
                    },
                    datalabels: {
                        // color: '#333',
                        // display: function (ctx) {
                        //     return ctx.dataset.data[ctx.dataIndex] > 0;
                        // },
                        // font: {
                        //     weight: 'bold'
                        // }
                        align: 'end',
                        anchor: 'end',
                        color: '#333',
                        display: function (ctx) {
                            return ctx.dataset.data[ctx.dataIndex] > 0;
                        },
                        font: function (context) {
                            var w = context.chart.width;
                            return {
                                size: 12,
                                weight: 'bold'
                            };
                        },
                        formatter: function (value, ctx) {
                            return value;
                        }
                    }
                },
                scales: {
                    x: {
                        stacked: true
                    },
                    y: {
                        stacked: true,
                        grid: {
                            display: false
                        }
                    }
                }

            },
            //plugins: [ChartDataLabels]
        };
        new Chart(document.getElementById('barChart'), config);
    }

    drawTurnoverAndWinLossByDate() {
        const labels = ['4/1', '4/2', '4/3', '4/4', '4/5', '4/6', '4/7', '4/8', '4/9', '4/10'];
        const data = {
            labels: labels,
            datasets: [
                {
                    label: 'Win/Loss',
                    borderColor: '#ff9f40',
                    backgroundColor: '#ffcf9f',
                    data: [11, 14, 10, 13, 8, 17, 13, 11, 19, 5],
                    fill: false,
                    datalabels: {
                        align: 'end',
                        anchor: 'end'
                    }
                },
                {
                    label: 'Turnover',
                    borderColor: '#36a2eb',
                    backgroundColor: '#9ad0f4',
                    data: [7, 6, 6, 10, 12, 27, 25, 8, 9, 12],
                    fill: true,
                    datalabels: {
                        align: 'end',
                        anchor: 'end'
                    }
                }
            ]
        };
        const config = {
            type: 'line',
            data: data,
            options: {
                plugins: {
                    title: {
                        text: 'Turnover & Win Loss By Date'
                    },
                    datalabels: {
                        align: function (context) {
                            var index = context.dataIndex;
                            var curr = context.dataset.data[index];
                            var prev = context.dataset.data[index - 1];
                            var next = context.dataset.data[index + 1];
                            return prev < curr && next < curr ? 'end' : prev > curr && next > curr ? 'start' : 'center';
                        },
                        backgroundColor: 'rgba(255, 255, 255, 0.7)',
                        borderColor: 'rgba(128, 128, 128, 0.7)',
                        borderRadius: 4,
                        borderWidth: 1,
                        color: function (context) {
                            var i = context.dataIndex;
                            var value = context.dataset.data[i];
                            var prev = context.dataset.data[i - 1];
                            var diff = prev !== undefined ? value - prev : 0;
                            return diff < 0 ? 'red' : diff > 0 ? 'blue' : '#333';
                        },
                        font: {
                            size: 11,
                            weight: 'bold'
                        },
                        offset: 8,
                        formatter: function (value, context) {
                            var i = context.dataIndex;
                            var prev = context.dataset.data[i - 1];
                            var diff = prev !== undefined ? prev - value : 0;
                            var glyph = diff < 0 ? '▲' : diff > 0 ? '▼' : '◆';
                            return glyph + ' ' + Math.round(value);
                        },
                        padding: 6
                    }
                },
                interaction: {
                    mode: 'index',
                    intersect: false
                },
                scales: {
                    x: {
                        title: {
                            display: true,
                            text: 'Day'
                        },
                        grid: {
                            display: false
                        }
                    },
                    y: {
                        title: {
                            display: true,
                            text: 'Millions'
                        }
                    }
                }
            },
            //plugins: [ChartDataLabels]
        };
        new Chart(document.getElementById('lineChart'), config);
    }
}

$(function () {
    const memberBettingBehavior = new MemberBettingBehavior($);
    memberBettingBehavior.init();
})