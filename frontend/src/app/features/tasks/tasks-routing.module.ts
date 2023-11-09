import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {TasksPageComponent} from "./tasks-page/tasks-page.component";

const routes: Routes = [{
    path: '',
    redirectTo: 'tasks',
    pathMatch: 'full',
}, {
    path: 'tasks',
    component: TasksPageComponent,
}];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class TasksRoutingModule {
}
