import { Headers, Http, Response } from '@angular/http';
import { Injectable } from '@angular/core';
import { ZenithEvent } from './zenith-event';
import 'rxjs/add/operator/toPromise';
import 'rxjs/add/operator/map';

@Injectable()
export class ZenithEventService {
  // private BASE_URL = "http://localhost:5000/api/events/";
  private BASE_URL = "http://zenithsocietycore.azurewebsites.net/api/events/";
  private 
  constructor(public _http: Http) { }
  getEvents(week: number) : Promise<ZenithEvent[]> {
    return this._http
    .get(this.BASE_URL + "week/" + week)
    .toPromise()
    .then(response => response.json() as ZenithEvent[]);
  }
  getWeekOf(week: number) : Promise<Date> {
    return this._http
    .get(this.BASE_URL + "weekOf/" + week)
    .toPromise()
    .then(response => new Date(response.json()));
  }
}
