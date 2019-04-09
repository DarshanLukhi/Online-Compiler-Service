import { Component } from '@angular/core';
import { IdeService } from './ide.service';
declare var $: any;
declare var ace: any;

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  constructor() { }
}
