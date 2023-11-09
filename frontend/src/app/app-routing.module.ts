import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {LayoutComponent} from "./shared/layout/layout.component";
import {AuthPageComponent} from "./features/auth/auth-page/auth-page.component";
import {TasksPageComponent} from "./features/tasks/tasks-page/tasks-page.component";

const routes: Routes = [
    {path: 'auth', component: AuthPageComponent},
    {
        path: 'tasks', component: LayoutComponent,
        children: [
            {path: '', component: TasksPageComponent}
        ]
    }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule {
}

