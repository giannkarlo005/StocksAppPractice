import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

import { RegisterUserData } from '../../models/register-user-data';
import { AccountService } from '../../services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  registerForm: FormGroup;
  isRegisterFormSubmitted: boolean = false;

  constructor(private _accountService: AccountService) {
    this.registerForm = new FormGroup({
      personName: new FormControl(null, [Validators.required]),
      emailAddress: new FormControl(null, [Validators.required, Validators.email]),
      phoneNumber: new FormControl(null, [Validators.required]),
      password: new FormControl(null, [Validators.required]),
      confirmPassword: new FormControl(null, [Validators.required])
    });
  }

  get register_personNameControl(): any {
    return this.registerForm.controls["personName"];
  }

  get register_emailAddressControl(): any {
    return this.registerForm.controls["emailAddress"];
  }

  get register_phoneNumberControl(): any {
    return this.registerForm.controls["phoneNumber"];
  }

  get register_passwordControl(): any {
    return this.registerForm.controls["password"];
  }

  get register_confirmPasswordControl(): any {
    return this.registerForm.controls["confirmPassword"];
  }

  onRegisterButtonClicked(): void {
    this.isRegisterFormSubmitted = true;
    if (!this.registerForm.valid) {
      return;
    }

    this._accountService.registerUser(this.registerForm.value).subscribe({
      next: (response: RegisterUserData) => {
        console.log(response);
      },
      error: (error: Error) => {
        console.log(error);
      },
      complete: () => {

      }
    });
  }
}
