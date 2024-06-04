"use strict"

class TurnoverByBetTypeChart {
    private chart: any;
    private canvasId: string;

    constructor(canvasId = 'canvas-turnover-by-bettype-chart') {
        this.canvasId = canvasId;
    }

    draw(betTypes: string[], lives: number[], nonLives: number[], mixes: number[]) {
        if (this.chart) {
            this.redraw(betTypes, lives, nonLives, mixes);
            return;
        }

        const canvas = document.getElementById(this.canvasId);
        const chartTitle = canvas.getAttribute('aria-label');

        const data = {
            labels: betTypes,
            datasets: [
                {
                    label: canvas.getAttribute('data-live'),
                    data: lives,
                    backgroundColor: 'rgb(46, 131, 204)'
                },
                {
                    label: canvas.getAttribute('data-nonlive'),
                    data: nonLives,
                    backgroundColor: 'rgb(242, 99, 80)'
                },
                {
                    label: canvas.getAttribute('data-mix'),
                    data: mixes,
                    backgroundColor: 'rgb(164, 164, 164)'
                }
            ]
        };

        const config = {
            type: 'bar',
            data: data,
            options: {
                barThickness: 50,
                plugins: {
                    title: {
                        text: chartTitle,
                        padding: {
                            bottom: 30
                        }
                    }
                },
                scales: {
                    x: {
                        stacked: true,
                        grid: {
                            display: false
                        }
                    },
                    y: {
                        stacked: true,
                        ticks: {
                            callback: (value) => SiteHelper.FormatCompactNumber(value)
                        }
                    }
                }
            }
        };

        this.chart = new Chart(canvas, config);
    }

    redraw(betTypes: string[], lives: number[], nonLives: number[], mixes: number[]) {
        this.chart.data.labels = betTypes
        this.chart.data.datasets[0].data = lives;
        this.chart.data.datasets[1].data = nonLives;
        this.chart.data.datasets[2].data = mixes;
        this.chart.update();
    }
}