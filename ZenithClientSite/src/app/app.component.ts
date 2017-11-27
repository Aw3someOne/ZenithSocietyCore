import { Component } from '@angular/core';
import { ZenithEventService } from './zenith-event.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers: [ZenithEventService],
})
export class AppComponent {
  title = 'app';
}
