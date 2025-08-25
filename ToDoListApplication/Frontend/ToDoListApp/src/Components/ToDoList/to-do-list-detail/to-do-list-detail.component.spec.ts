import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ToDoListDetailComponent } from './to-do-list-detail.component';

describe('ToDoListDetailComponent', () => {
  let component: ToDoListDetailComponent;
  let fixture: ComponentFixture<ToDoListDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ToDoListDetailComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ToDoListDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
