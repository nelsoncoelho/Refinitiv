import { Component, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;
  @Output() userEvent = new EventEmitter<any>();

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  sendUser() {
    this.userEvent.emit(null);
  }

  logout() {
    console.log("here");
    this.sendUser();
    localStorage.removeItem("User");
  }

  getUser(): string {
    return localStorage.getItem("User");
  }
}
