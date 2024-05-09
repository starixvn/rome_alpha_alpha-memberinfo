"use strict"

class TurnoverBySportChart {
    private chart: any;
    private canvasId: string;

    constructor(canvasId = 'canvas-turnover-by-sport-chart') {
        this.canvasId = canvasId;
    }

    draw(sportTypes: string[], turnovers: number[]) {
        if (this.chart) {
            this.redraw(sportTypes, turnovers);
            return;
        }

        const canvas = document.getElementById(this.canvasId);
        const chartTitle = canvas.getAttribute('aria-label');

        const data = {
            labels: sportTypes,
            datasets: [
                {
                    data: turnovers,
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
                        text: chartTitle,
                        padding: {
                            bottom: 25
                        }
                    },
                    legend: {
                        title: {
                            display: true,
                            padding: {
                                bottom: 2
                            }
                        }
                    },
                    datalabels: {
                        color: '#333',
                        font: {
                            weight: 'bold'
                        },
                        formatter: (value, ctx) => `${this.calculatePercentage(ctx, value)}%`,
                        display: 'auto',
                        labels: {
                            index: {
                                anchor: (ctx) => this.getPercentagePosition(ctx),
                                align: (ctx) => this.getPercentagePosition(ctx)
                            }
                        }
                    }
                }
            },
            plugins: [ChartDataLabels]
        };

        this.chart = new Chart(canvas, config);
    }

    redraw(sportTypes: string[], turnovers: number[]) {
        this.chart.data.labels = sportTypes
        this.chart.data.datasets[0].data = turnovers;
        this.chart.update();
    }

    private getPercentagePosition(ctx) {
        return this.calculatePercentage(ctx, ctx.dataset.data[ctx.dataIndex]) >= 4 ? 'center' : 'end';
    }

    private calculatePercentage(ctx, value: number) {
        let sum = 0;
        const dataArray = ctx.chart.data.datasets[0].data;
        dataArray.map(data => sum += data);

        return Math.round((value * 100) / sum);
    }
}
