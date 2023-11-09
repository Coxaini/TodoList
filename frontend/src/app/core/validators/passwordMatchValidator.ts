import {AbstractControl, FormGroup, ValidationErrors, ValidatorFn} from "@angular/forms";

export function passwordMatchValidator(controlName: string, matchingControlName: string): ValidatorFn {

  return (control: AbstractControl): ValidationErrors | null => {
    const password = control.get(controlName);
    const matchingPassword = control.get(matchingControlName);

    if (password === null || matchingPassword === null) throw new Error('passwordMatchValidator: control or matchingControl is null');

    if (password.value !== matchingPassword.value) {
      matchingPassword.setErrors({passwordMismatch: true});
      return {passwordMismatch: true};
    } else {
      matchingPassword.setErrors(null);
      return null;
    }
  };
}
