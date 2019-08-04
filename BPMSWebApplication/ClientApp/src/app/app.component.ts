import { Component, AfterViewInit, ViewChild } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

    
  title = 'app';
  user: string;

  sendUser(event: string) {
    this.user = event;
    localStorage.setItem("User", this.user);
  }
}
