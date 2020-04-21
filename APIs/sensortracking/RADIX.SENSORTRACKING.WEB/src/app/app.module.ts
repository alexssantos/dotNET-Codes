import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

// PAGES ====
import { HeaderComponent } from './pages/shared/header/header.component';
import { HomeComponent } from './pages/home/home.component';
import { GraficoLinhaComponent } from './pages/shared/grafico-linha/grafico-linha.component';
import { TabelaSensoresComponent } from './pages/shared/tabela-sensores/tabela-sensores.component';

// ANGULAR MATERIAL ===
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatTableModule } from '@angular/material/table';
import { MatInputModule } from '@angular/material/input';
import { MatPaginatorModule } from '@angular/material/paginator';
import {MatButtonModule} from '@angular/material/button';

// LIBS ===
import { ChartsModule } from 'ng2-charts';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

@NgModule({
	declarations: [
		AppComponent,
		HeaderComponent,
		HomeComponent,
		GraficoLinhaComponent,
		TabelaSensoresComponent
	],
	imports: [
		BrowserModule,
		AppRoutingModule,
		// import HttpClientModule after BrowserModule.
		HttpClientModule,
		BrowserAnimationsModule,
		MatToolbarModule,
		MatTableModule,
		MatInputModule,
		MatButtonModule,
		MatPaginatorModule,
		ChartsModule,
		FormsModule
	],
	providers: [],
	bootstrap: [AppComponent]
})
export class AppModule { }
