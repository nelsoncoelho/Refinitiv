import { Component, Inject, Output, EventEmitter } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
})
export class LoginComponent {
  registerForm = {
    username: "",
    password: "",
    name: ""
  };

  loginForm = {
    username: "",
    password: "",
  };

  user: string;
  @Output() userEvent = new EventEmitter<any>();

  constructor(public http: HttpClient, @Inject('BASE_URL') public baseUrl: string) {
    
  }

  sendUser() {
    this.userEvent.emit(this.user);
  }

  register() {
    this.http.post<Boolean>(this.baseUrl + 'api/SampleData/Register', { username: this.registerForm.username, password: this.registerForm.password, name: this.registerForm.name }).subscribe(response => {
      if (response) {
        window.alert("Account registered.");
      }
      else {
        window.alert("Username already exists.");
      }
    }, error => console.error(error));
  }

  login() {
    this.http.post<string>(this.baseUrl + 'api/SampleData/Login', { username: this.loginForm.username, password: this.loginForm.password }).subscribe(response => {
      if (response != null) {
        this.user = response;
        this.sendUser();
      }
      else {
        window.alert("Incorrect username or password");
      }
    }, error => console.error(error));
  }
}


