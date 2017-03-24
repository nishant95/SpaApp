import { NgModule } from '@angular/core';
import { RouterModule, Routes }  from '@angular/router';
import { HomeComponent, FetchDataComponent, CounterComponent, PersonComponent } from './components';
import { AddPersonComponent, ViewPersonsComponent, EditPersonComponent } from './components/person';

const appRoutes : Routes =
[
    { path: 'home', component: HomeComponent },
    { path: 'counter', component: CounterComponent },
    { path: 'fetch-data', component: FetchDataComponent },
    { path: '', redirectTo: 'home', pathMatch: 'full' },
    { path: './', redirectTo: 'home', pathMatch: 'full' },
    { path: 'person', component: PersonComponent,
        children:
        [
            { path: 'view', component: ViewPersonsComponent },
            { path: 'add', component: AddPersonComponent },
            { path: 'edit/:id', component: EditPersonComponent },
            { path: './', redirectTo: 'view', pathMatch: 'full' },
            { path: '', redirectTo: 'view', pathMatch: 'full' }
        ]
    },
    
]

@NgModule({
  imports: [
    RouterModule.forRoot(appRoutes)
  ],
  exports: [
    RouterModule
  ]
})
export class AppRoutingModule { }
