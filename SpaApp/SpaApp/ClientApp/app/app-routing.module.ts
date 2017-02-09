import { NgModule } from '@angular/core';
import { RouterModule, Routes }  from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { FetchDataComponent } from './components/fetchdata/fetchdata.component';
import { CounterComponent } from './components/counter/counter.component';
import { PersonComponent } from './components/person/person.component';
import { AddPersonComponent } from './components/person/add-person/add-person.component';
import { ViewPersonsComponent } from './components/person/view-persons/view-persons.component';

const appRoutes : Routes =
[
    { path: '', redirectTo: 'home', pathMatch: 'full' },
    { path: 'home', component: HomeComponent },
    { path: 'counter', component: CounterComponent },
    { path: 'fetch-data', component: FetchDataComponent },
    { path: 'person', component: PersonComponent,
        children:
        [
            { path: '', redirectTo: 'View', pathMatch: 'full' },
            { path: 'View', component: ViewPersonsComponent },
            { path: 'Add', component: AddPersonComponent }
        ]
    },
    { path: '**', redirectTo: 'home' }
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
