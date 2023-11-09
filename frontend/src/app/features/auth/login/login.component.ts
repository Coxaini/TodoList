import {Component} from '@angular/core';
import {AuthenticationService} from "../../../core/services/auth/authentication.service";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {Router} from "@angular/router";

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss']
})
export class LoginComponent {

    loginForm: FormGroup;

    constructor(private authService: AuthenticationService, fb: FormBuilder, private router: Router) {
        this.loginForm = fb.group({
            username: ['', [Validators.required]],
            password: ['', [Validators.required]]
        });

        this.username.valueChanges.subscribe(() => {
            this.password.setErrors({incorrect: null});
            this.password.updateValueAndValidity();
        });
    }

    get username() {
        return this.loginForm.get('username')!;
    }

    get password() {
        return this.loginForm.get('password')!;
    }

    public login() {
        this.authService.login(this.username.value, this.password.value).subscribe({
            next: () => {
                this.router.navigate(['/']);
            },
            error: (err: any) => {
                if (err.status === 400) {
                    this.password.setErrors({incorrect: true});
                } else if (err.status === 404) {
                    this.username.setErrors({incorrect: true});
                }
            }
        });
    }
}
