import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';

import {AuthRoutingModule} from './auth-routing.module';
import {AuthPageComponent} from "./auth-page/auth-page.component";
import {LoginComponent} from "./login/login.component";
import {RegisterComponent} from "./register/register.component";
import {FaIconLibrary, FontAwesomeModule} from "@fortawesome/angular-fontawesome";
import {faTableList} from "@fortawesome/free-solid-svg-icons";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";


@NgModule({
    declarations: [AuthPageComponent, LoginComponent, RegisterComponent],
    imports: [
        CommonModule,
        AuthRoutingModule,
        FontAwesomeModule,
        ReactiveFormsModule,
        FormsModule
    ]
})
export class AuthModule {
    constructor(library: FaIconLibrary) {
        library.addIcons(faTableList);
    }
}
