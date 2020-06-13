import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from './../../environments/environment';
import { Observable } from 'rxjs';


@Injectable({
	providedIn: 'root'
})
export class SensorService {

	private apiUrl: string;

	constructor(
		private http: HttpClient
	) { 
		this.apiUrl = environment.apiUrl;
	}

	public getTableTotais(): Observable<any> {
		let url = this.apiUrl + "/sensor/totais";
		return this.http.get(url);
	}

	public getTableSensoresPaginado(pagina: number ,paginaQtd: number , buscaAtual:string, busca: string): Observable<any> {
		let paramPag= `pagina=${pagina}`;
		let paramPagQtd = `paginaTamanho=${paginaQtd}`;
		let paramBuscaAtual = (buscaAtual) ? `buscaCorrente=${buscaAtual}` : null;
		let paramBusca = (busca) ? `busca=${busca}` : null;

		let queryStr = "?" + paramPag + "&" + paramPagQtd + ((paramBuscaAtual) ? "&" + paramBuscaAtual : "") + ((paramBusca) ? "&" + paramBusca : "");
		let url = this.apiUrl + "/sensor/paginado" + queryStr;
		return this.http.get(url);
	}

	public getGraficoEventosTotais(): Observable<any> {
		let url = this.apiUrl + "/sensor/graficoEventos";
		return this.http.get(url);
	}


}
