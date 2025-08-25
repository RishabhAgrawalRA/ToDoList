import { Component, OnInit } from '@angular/core';
import {
  CalendarModule,
  DateAdapter,
  CalendarUtils,
  CalendarA11y,
  CalendarDateFormatter,
  CalendarEventTitleFormatter,
  CalendarEvent,
} from 'angular-calendar';
import { adapterFactory } from 'angular-calendar/date-adapters/date-fns';
import { CommonModule } from '@angular/common';
import { ToDoListService } from '../../../services/to-do-list.service';


@Component({
  selector: 'app-calendar',
  standalone: true,
  imports: [CommonModule, CalendarModule],
  providers: [
    { provide: DateAdapter, useFactory: adapterFactory },
    CalendarUtils,
    CalendarA11y,
    CalendarDateFormatter,
    CalendarEventTitleFormatter
  ],
  templateUrl: './calendar.component.html',
  styleUrls: ['./calendar.component.css']
})
export class CalendarComponent implements OnInit {
  viewDate: Date = new Date();
  minMonth:number=0;
  maxMonth:number=11;
  interation:number=this.viewDate.getMonth();
  selectedDay:any = null;
  events: CalendarEvent[] = [];

  constructor(private _service:ToDoListService){}

  ngOnInit(){
    this.fetchEvents();
  }

  preMonth(){
    if(this.interation>this.minMonth)
      {
        this.interation=this.interation-1;
        this.viewDate = new Date(new Date().getFullYear(), this.interation, 1)
        this.fetchEvents();
      }
  }

  nextMonth(){
    if(this.interation<this.maxMonth)
    {
      this.interation=this.interation+1;
      this.viewDate= new Date(new Date().getFullYear(), this.interation, 1)
      this.fetchEvents();
    }
  }

  fetchEvents(){
    this._service.getToDoListByMonth(this.interation+1).subscribe({
      next : response => { 
        this.events = [
        ...response.map(item => ({
        start: new Date(item.createdAt),
        title: item.title,
        meta: { description: item.description, isCompleted: item.isCompleted }
        }))
        ];
    },
      error : err => console.log(err)
    });
  }

  handleDayClick(day:any){
    this.selectedDay=day;
  }
}
