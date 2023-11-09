import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';

import {TasksRoutingModule} from './tasks-routing.module';
import {TasksPageComponent} from "./tasks-page/tasks-page.component";
import {TaskItemComponent} from "./task-item/task-item.component";
import {FaIconLibrary, FontAwesomeModule} from "@fortawesome/angular-fontawesome";
import {faPenToSquare, faTrash} from "@fortawesome/free-solid-svg-icons";
import {ReactiveFormsModule} from "@angular/forms";


@NgModule({
    declarations: [TasksPageComponent, TaskItemComponent],
    imports: [
        CommonModule,
        TasksRoutingModule,
        FontAwesomeModule,
        ReactiveFormsModule
    ]
})
export class TasksModule {
    constructor(library: FaIconLibrary) {
        library.addIcons(faTrash, faPenToSquare);
    }
}
