import { Component, OnInit, ViewChild } from '@angular/core';
import { ChartDataSets, ChartOptions } from 'chart.js';
import { Color, BaseChartDirective, Label } from 'ng2-charts';
import { SensorService } from 'src/app/services/sensor.service';
import { DatePipe } from '@angular/common';
import { Observable, interval } from 'rxjs';


@Component({
	selector: 'app-grafico-linha',
	templateUrl: './grafico-linha.component.html',
	styleUrls: ['./grafico-linha.component.scss'],
	providers: [DatePipe]
})
export class GraficoLinhaComponent implements OnInit {

	public lineChartData: ChartDataSets[] = [		
		{ 
			data: [], 
			label: 'Total Eventos/segundo'			
		}
	];
	public lineChartLabels: Label[] = [];
	public lineChartOptions: (ChartOptions & { annotation: any }) = {
		responsive: true,
		scales: {
			// We use this empty structure as a placeholder for dynamic theming.
			xAxes: [{}],
			yAxes: [
				{
					id: 'y-axis-0',
					position: 'left',
				},
				{
					id: 'y-axis-1',
					position: 'right',
					gridLines: {
						color: 'rgba(255,0,0,0.3)',
					},
					ticks: {
						fontColor: 'black',
					}
				}
			]
		},
		annotation: {
			annotations: [
				{
					type: 'line',
					mode: 'vertical',
					scaleID: 'x-axis-0',
					value: 'March',
					borderColor: 'orange',
					borderWidth: 2,
					label: {
						enabled: true,
						fontColor: 'orange',
						content: 'LineAnno'
					}
				},
			],
		},
	};
	public lineChartColors: Color[] = [		
		{ // red
			backgroundColor: 'rgba(0,0,200,0.3)',
			borderColor: 'blue',
			pointBackgroundColor: 'rgba(255,255,255,1)',
			pointBorderColor: '#fff',
			pointHoverBackgroundColor: '#fff',
			pointHoverBorderColor: 'rgba(0,0,177,0.8)'
		}
	];
	public lineChartLegend = true;
	public lineChartType = 'line';	

	public autoUpdateGraph: Observable<number>;

	@ViewChild(BaseChartDirective, { static: true }) chart: BaseChartDirective;
	

	constructor(
		private sensorServ: SensorService,
		public datepipe: DatePipe
	) { }

	ngOnInit(): void {
		this.autoUpdateGraph = interval(15*1000);
		this.autoUpdateGraph.subscribe(() => this.getGraficoData());
	}

	// events
	public chartClicked({ event, active }: { event: MouseEvent, active: {}[] }): void {
		console.log(event, active);
	}

	public chartHovered({ event, active }: { event: MouseEvent, active: {}[] }): void {
		//console.log(event, active);
	}

	public hideOne() {
		const isHidden = this.chart.isDatasetHidden(1);
		this.chart.hideDataset(1, !isHidden);
	}

	public pushOne() {
		let lineData = this.lineChartData[0]; 		
		const num = 50;				
		let label: Label = "Teste";
		
		lineData.data.push(num);
		this.lineChartLabels.push(label);
		
		lineData.data.shift()
		this.lineChartLabels.shift();
	}

	public changeColor() {
		this.lineChartColors[2].borderColor = 'green';
		this.lineChartColors[2].backgroundColor = `rgba(0, 255, 0, 0.3)`;
	}

	public changeLabel() {
		this.lineChartLabels[2] = ['1st Line', '2nd Line'];
		// this.chart.update();
	}



	public getGraficoData(): void {
		this.sensorServ.getGraficoEventosTotais().subscribe(
			(res) => {
				let data: number[] =[];
				let labels: Label[] = [];

				res.map((graph) => {
					let myDate = new Date(graph.dataEventos);
					let dateFormated = this.datepipe.transform(myDate, 'dd/MM/yyyy H:mm:ss');
					labels.push(dateFormated);
					data.push(graph.totalEventos);
				});

				this.lineChartData[0].data = data;
				this.lineChartLabels = labels;
			},
			(error) => {
				console.error(error);
			}
		)
	}
}
