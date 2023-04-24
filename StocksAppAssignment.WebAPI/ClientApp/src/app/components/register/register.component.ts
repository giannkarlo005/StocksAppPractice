import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

import { AccountService } from '../../services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup;

  constructor(private accountService: AccountService) {
    this.registerForm = new FormGroup({
      personName: new FormControl(null, [Validators.required])
    });
  }

  ngOnInit(): void {
  }

  onSubmitButtonClicked(): void {
    this.accountService.registerUser(this.registerForm.value).subscribe({
      next: (response: any) => {
        console.log(response);
      },
      error: (error: Error) => {

      },
      complete: () => {

      }
    });
  }
}
