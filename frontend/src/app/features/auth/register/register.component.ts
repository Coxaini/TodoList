import {Component} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {AuthenticationService} from "../../../core/services/auth/authentication.service";
import {Router} from "@angular/router";
import {passwordMatchValidator} from "../../../core/validators/passwordMatchValidator";


@Component({
    selector: 'app-register',
    templateUrl: './register.component.html',
    styleUrls: ['./register.component.scss']
})
export class RegisterComponent {
    constructor(private fb: FormBuilder, private authService: AuthenticationService, private router: Router) {
        this.registerForm = fb.group({
            username: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(20)]],
            password: ['', [Validators.required, Validators.pattern('^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]{8,}$')]],
            passwordConfirm: ['', [Validators.required]]
        }, {validators: [passwordMatchValidator('password', 'passwordConfirm')]});
    }

    public registerForm: FormGroup;

    public register(): void {
        this.authService.register(this.username.value, this.password.value)
            .subscribe({
                next: () => {
                    this.router.navigate(['/']);
                },
                error: (err: any) => {
                    if (err.status === 409) {
                        this.username.setErrors({taken: true});
                    }
                }
            });
    }

    get username() {
        return this.registerForm.get('username')!;
    }

    get password() {
        return this.registerForm.get('password')!;
    }

    get passwordConfirm() {
        return this.registerForm.get('passwordConfirm')!;
    }
}
