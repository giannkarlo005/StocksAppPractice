import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router'

import { AccountService } from '../../services/account.service';
import { AppService } from '../../services/app-service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  loginForm: FormGroup;
  isLoginFormSubmitted: boolean = false;

  constructor(private _accountService: AccountService,
              private _appService: AppService,
              private _router: Router) {
    this.loginForm = new FormGroup({
      emailAddress: new FormControl(null, [Validators.required, Validators.email]),
      password: new FormControl(null, [Validators.required]),
    });
  }

  get login_emailAddressControl(): any {
    return this.loginForm.controls["emailAddress"];
  }

  get login_passwordControl(): any {
    return this.loginForm.controls["password"];
  }

  onLoginButtonClicked(): void {
    this.isLoginFormSubmitted = true;
    if (!this.loginForm.valid) {
      return;
    }

    this._accountService.loginUser(this.loginForm.value).subscribe({
      next: (response: any) => {
        // If it returns the isSucceeded sign im result
        if (response === true) {
          this._appService.setIsUserLoggedIn(true);
          this._router.navigate(['/stocks']);
        }
      },
      error: (error: Error) => {
        console.log(error);
      },
      complete: () => {

      }
    });
  }
}
