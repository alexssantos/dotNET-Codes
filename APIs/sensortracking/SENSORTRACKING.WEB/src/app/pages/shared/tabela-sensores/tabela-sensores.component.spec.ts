import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TabelaSensoresComponent } from './tabela-sensores.component';

describe('TabelaSensoresComponent', () => {
  let component: TabelaSensoresComponent;
  let fixture: ComponentFixture<TabelaSensoresComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TabelaSensoresComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TabelaSensoresComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
