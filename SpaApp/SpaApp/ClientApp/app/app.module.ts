import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { UniversalModule } from 'angular2-universal';
import { FormsModule } from '@angular/forms';
import { AppComponent } from './components/app/app.component'
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { FetchDataComponent } from './components/fetchdata/fetchdata.component';
import { CounterComponent } from './components/counter/counter.component'; 
import { AppRoutingModule } from './app-routing.module';
import { PersonComponent } from './components/person/person.component';
import { AddPersonComponent } from './components/person/add-person/add-person.component';
import { ViewPersonsComponent } from './components/person/view-persons/view-persons.component';
import { PersonService } from './services';
import { SecurityService } from './services';
import { APP_CONFIG, AppConfig } from './app-config';
import { WINDOW_REF, WindowRef } from './services';

@NgModule({
    imports: [
        UniversalModule, // Must be first import. This automatically imports BrowserModule, HttpModule, and JsonpModule too.
        AppRoutingModule,
        FormsModule
    ],
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
    providers: [
        PersonService,
        SecurityService,
        { provide: APP_CONFIG, useValue: AppConfig },
        { provide: WINDOW_REF, useValue: WindowRef }
    ]
})
export class AppModule {
}
