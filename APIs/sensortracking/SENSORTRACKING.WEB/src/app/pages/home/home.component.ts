import { Component, OnInit } from '@angular/core';
import { SensorService } from 'src/app/services/sensor.service';
import { TabelaTotal } from 'src/app/models/tabela-total.model';
import { SensorSimples } from 'src/app/models/sensor-simples.model';

@Component({
	selector: 'app-home',
	templateUrl: './home.component.html',
	styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

	public isOpen:boolean = true;
	public tabelaTotaisList: Array<TabelaTotal> = [];

	constructor(
		private sensorServ: SensorService
	) { }

	ngOnInit(): void {
		this.getTableTotaisData();
	}

	subTable(){
		this.isOpen = (!this.isOpen);
	}

	public getTableTotaisData():void {
		this.tabelaTotaisList = [];

		this.sensorServ.getTableTotais().subscribe(
			(res) => {
				let regKeys = Object.keys(res);
				regKeys.map((regKey) => {
					let regiaoTotal = 0;
					let sensoresArray: SensorSimples[] = [];

					let sensorDict = res[regKey];
					let sensorKeys =  Object.keys(sensorDict);
					sensorKeys.map((sensKey) => {
						let bean: SensorSimples = {
							nome: sensKey,
							total: sensorDict[sensKey]
						}
						regiaoTotal += bean.total;
						sensoresArray.push(bean);
					})

					let bean: TabelaTotal = {
						regiaoNome: regKey,
						regiaoTotal: regiaoTotal,
						sensores: sensoresArray,
						rowOpen: false
					}
					this.tabelaTotaisList.push(bean);
				});
			},
			(error: any) => {
				console.error(error);
				this.tabelaTotaisList = [];
			}
		)
	}

	public fechaAbaRegiaoTabela(regiao: TabelaTotal): void{
		regiao.rowOpen = !regiao.rowOpen;
	}
}
