import {Component} from '@angular/core';
import {AuthenticationService} from "../../core/services/auth/authentication.service";
import {Router, RouterModule} from "@angular/router";
import {CommonModule} from "@angular/common";

@Component({
    selector: 'app-layout',
    standalone: true,
    templateUrl: './layout.component.html',
    styleUrls: ['./layout.component.scss'],
    imports: [CommonModule, RouterModule],
})
export class LayoutComponent {
    constructor(private authService: AuthenticationService, private router: Router) {
    }

    public logout(): void {
        this.authService.logout().subscribe(() => {
            this.router.navigate(['/auth/login']);
        });
    }
}
