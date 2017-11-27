import { Component, OnInit } from '@angular/core';
import { ZenithEventService } from '../zenith-event.service';
import { ZenithEvent } from '../zenith-event';

@Component({
  selector: 'app-zenith-event',
  templateUrl: './zenith-event.component.html',
  styleUrls: ['./zenith-event.component.css']
})
export class ZenithEventComponent implements OnInit {

  events: ZenithEvent[];
  groupedEvents: ZenithEvent[][];
  week: number = 0;
  weekOf: Date;

  constructor(public eventSrv: ZenithEventService) { }

  ngOnInit() {
    this.getEvents(this.week);
  }

  previous() {
    this.getEvents(--this.week);
  }

  next() {
    this.getEvents(++this.week);
  }

  getEvents(week: number) {
    this.eventSrv.getEvents(week)
      .then(events => {
        this.events = events
        this.groupedEvents = new Array<Array<ZenithEvent>>();
        for (let i = 0; i < 7; i++) {
          this.groupedEvents[i] = new Array<ZenithEvent>();
        }
        this.events.forEach(element => {
          this.groupedEvents[new Date(element.eventDate).getDay() - 1].push(element)
        });
      });
      this.eventSrv.getWeekOf(week)
      .then(weekOf => { this.weekOf = weekOf });
  }

}
