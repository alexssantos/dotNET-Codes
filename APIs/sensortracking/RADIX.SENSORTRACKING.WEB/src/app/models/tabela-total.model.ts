import { SensorSimples } from './sensor-simples.model';

export class TabelaTotal {
	public regiaoNome: string;
	public regiaoTotal: number;
	public sensores: SensorSimples[];
	public rowOpen: boolean = true;
}

