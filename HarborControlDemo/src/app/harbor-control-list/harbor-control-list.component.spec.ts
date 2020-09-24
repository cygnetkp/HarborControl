import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HarborControlListComponent } from './harbor-control-list.component';

describe('HarborControlListComponent', () => {
  let component: HarborControlListComponent;
  let fixture: ComponentFixture<HarborControlListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HarborControlListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HarborControlListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
