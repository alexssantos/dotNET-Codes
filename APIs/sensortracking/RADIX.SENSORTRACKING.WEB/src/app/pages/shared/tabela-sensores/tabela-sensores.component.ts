import { Component, OnInit, ViewChild, OnDestroy } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { SensorService } from 'src/app/services/sensor.service';
import { SensorData } from 'src/app/models/sensor-data.model';
import { interval, Observable, Subscription } from 'rxjs';


@Component({
	selector: 'app-tabela-sensores',
	templateUrl: './tabela-sensores.component.html',
	styleUrls: ['./tabela-sensores.component.scss']
})
export class TabelaSensoresComponent implements OnInit {

	displayedColumns: string[] = ['pais', 'regiao', 'sensorNome', 'valor', 'data'];
	dataSource: MatTableDataSource<SensorData>;
	buscaAtual: string;
	inputBusca: any;
	totalItens: number = 0;
	timeAtualizaTabela = (10*1000);
	sensoresPeriodicoUpdate: Observable<number>;

	@ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;	

	constructor(
		public sensorServ: SensorService
	) {		
		this.dataSource = new MatTableDataSource([]);
	}

	ngOnInit() {
		this.dataSource.paginator = this.paginator;		
		this.paginator.page.subscribe(() => this.getTabelaSensores());
		this.paginator.pageSize = 5;		
		this.getTabelaSensores();
		
		this.sensoresPeriodicoUpdate = interval(this.timeAtualizaTabela);
		this.sensoresPeriodicoUpdate.subscribe(() => this.getTabelaSensores());
	}
	
	applyFilter() {		
		if (this.dataSource.paginator) {
		 	this.dataSource.paginator.firstPage();
		}		
		this.getTabelaSensores(this.inputBusca.toString());
	}

	public getTabelaSensores(busca?: string): void {
		let pagina = this.paginator.pageIndex + 1;
		let paginaQtd = this.paginator.pageSize;

		this.sensorServ.getTableSensoresPaginado(
			pagina, paginaQtd, this.buscaAtual, busca
		).subscribe(
			(res) => {				
				this.totalItens = res.itensTotal;
				this.paginator.pageIndex = res.paginaAtual - 1;			
				
				let sensores: SensorData[] = res.itens.map((item) => <SensorData>item);

				this.dataSource = new MatTableDataSource(sensores);
			},
			(error: any) => {
				console.error(error);
			},
			() => {
				this.buscaAtual = this.inputBusca;
			}
		)
	}
}
