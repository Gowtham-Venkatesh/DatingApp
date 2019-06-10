import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MembersListComponent } from './members-list/members-list.component';
import { ListsComponent } from './lists/lists.component';
import { MessageComponent } from './message/message.component';
import { AuthGuard } from './_guards/auth.guard';

export const appRouter: Routes = [
    {path: '', component: HomeComponent},
    { path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
        {path: 'members', component: MembersListComponent, canActivate: [AuthGuard]},
        {path: 'lists', component: ListsComponent},
        {path: 'message', component: MessageComponent},

    ]
        },
    {path: '**', redirectTo: '', pathMatch: 'full'},
];
