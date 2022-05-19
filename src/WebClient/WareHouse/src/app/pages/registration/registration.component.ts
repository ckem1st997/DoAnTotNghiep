import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NotifierService } from 'angular-notifier';
import { first } from 'rxjs';
import { AuthenticationService } from 'src/app/extension/Authentication.service';
import { LoginValidator } from 'src/app/validator/LoginValidator';
import { RegistrationValidator } from 'src/app/validator/RegistrationValidator';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent implements OnInit {
  loginForm!: FormGroup;
  loading = false;
  submitted = false;
  error = '';

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private authenticationService: AuthenticationService,
    private notifierService: NotifierService
  ) {
    // redirect to home if already logged in
    if (this.authenticationService.userCheck) {
      this.router.navigate(['page']);
    }
  }

  ngOnInit() {
    this.loginForm = this.formBuilder.group({
      username: 'user@example.com',
      password: '',
      repassword: '',
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
    var test = new RegistrationValidator();
    var msg = test.validate(this.loginForm.value);
    var check = JSON.stringify(msg) == '{}';
    if (check == true) {
      this.loading = true;
      this.authenticationService.registration(this.loginForm.value['username'], this.loginForm.value['password'], this.loginForm.value['repassword'])
        .pipe(first())
        .subscribe({
          next: (x) => {
            if (x.success) {
              //  this.notifierService.notify('warning', x.message);
              //  const returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
              this.notifierService.notify('success', 'Đăng ký thành công !');

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