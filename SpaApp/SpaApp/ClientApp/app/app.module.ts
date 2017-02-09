import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { UniversalModule } from 'angular2-universal';
import { AppComponent } from './components/app/app.component'
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { FetchDataComponent } from './components/fetchdata/fetchdata.component';
import { CounterComponent } from './components/counter/counter.component'; 
import { AppRoutingModule } from './app-routing.module';
import { PersonComponent } from './components/person/person.component';
import { AddPersonComponent } from './components/person/add-person/add-person.component';
import { ViewPersonsComponent } from './components/person/view-persons/view-persons.component';
import { PersonService } from './services/person.service';

@NgModule({
    bootstrap: [ AppComponent ],
    declarations: [
        AppComponent,
        NavMenuComponent,
        CounterComponent,
        FetchDataComponent,
        HomeComponent,
        PersonComponent,
        AddPersonComponent,
        ViewPersonsComponent
    ],
    imports: [
        UniversalModule, // Must be first import. This automatically imports BrowserModule, HttpModule, and JsonpModule too.
        AppRoutingModule
    ],
    providers: [
        PersonService
    ]
})
export class AppModule {
}
