import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NotifierService } from 'angular-notifier';
import { first } from 'rxjs';
import { AuthenticationService } from 'src/app/extension/Authentication.service';
import { SignalRService } from 'src/app/service/SignalR.service';
import { LoginValidator } from 'src/app/validator/LoginValidator';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  loginForm!: FormGroup;
  loading = false;
  submitted = false;
  error = '';

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private authenticationService: AuthenticationService,
    private notifierService: NotifierService,
    private signalRService: SignalRService
  ) {
    // redirect to home if already logged in
    if (this.authenticationService.userCheck) {
      this.router.navigate(['page']);
    }
  }

  ngOnInit() {
    this.loginForm = this.formBuilder.group({
      username: 'admin@gmail.com',
      password: '123456',
      remember: true
    });
  }

  // convenience getter for easy access to form fields
  get f() { return this.loginForm.controls; }

  onSubmit() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.loginForm.invalid) {
      return;
    }
    var test = new LoginValidator();
    var msg = test.validate(this.loginForm.value);
    var check = JSON.stringify(msg) == '{}';
    if (check == true) {
      this.loading = true;
      this.authenticationService.login(this.loginForm.value['username'], this.loginForm.value['password'], this.loginForm.value['remember'])
        .pipe(first())
        .subscribe({
          next: (x) => {
            if (x.success) {
              //  this.notifierService.notify('warning', x.message);
              //  const returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
              this.notifierService.notify('success', 'Đăng nhập thành công !');
              this.signalRService.startConnection();

              //   this.router.navigate([returnUrl]);
              this.router.navigate(['page']);
            }

          },
          error: error => {
            this.error = error;
            this.loading = false;
            // if (error.error.errors.length === undefined)
            //   this.notifierService.notify('error', error.error.message);
            // else
            //   this.notifierService.notify('error', error.error);
          }
        });
    }
    else {
      var message = '';
      for (const [key, value] of Object.entries(msg)) {
        message = message + " " + value;
      }
      this.notifierService.notify('error', message);
    }




  }
}