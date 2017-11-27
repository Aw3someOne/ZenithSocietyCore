import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ZenithEventComponent } from './zenith-event.component';

describe('ZenithEventComponent', () => {
  let component: ZenithEventComponent;
  let fixture: ComponentFixture<ZenithEventComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ZenithEventComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ZenithEventComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
