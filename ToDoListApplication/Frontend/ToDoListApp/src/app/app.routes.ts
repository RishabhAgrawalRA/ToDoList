import { Routes } from '@angular/router';
import { AboutComponent } from '../Components/About/about/about.component';
import { ToDoListComponent } from '../Components/ToDoList/to-do-list/to-do-list.component';
import { ToDoListAddComponent } from '../Components/ToDoList/to-do-list-add/to-do-list-add.component';
import { ToDoListDetailComponent } from '../Components/ToDoList/to-do-list-detail/to-do-list-detail.component';

export const routes: Routes = [
    {path:'', component:AboutComponent},
    {path: 'calendar', loadComponent: () => import('../Components/Calendar/calendar/calendar.component').then(m => m.CalendarComponent)},
    {path:'todolist', component: ToDoListComponent},
    {path:'todolist/add', component: ToDoListAddComponent},
    {path:'todolist/:id', component: ToDoListDetailComponent}
];
