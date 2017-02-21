import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { BrowserModule } from '@angular/platform-browser';
import { UniversalModule } from 'angular2-universal';
import { FormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';

import { AppComponent, PersonComponent, CounterComponent, FetchDataComponent, HomeComponent, NavMenuComponent } from './components';
import { AddPersonComponent, ViewPersonsComponent } from './components/person';
import { PersonService, SecurityService, WindowBrowser, IWindow } from './services';
import { SpaEmailValidator } from './directives';
import { APP_CONFIG, AppConfig } from './app-config';
import { DatepickerModule } from 'angular2-material-datepicker'
import { MyDatePickerModule } from 'mydatepicker';

@NgModule({
    imports: [
        UniversalModule, // Must be first import. This automatically imports BrowserModule, HttpModule, and JsonpModule too.
        AppRoutingModule,
        FormsModule,
        DatepickerModule,
        MyDatePickerModule
    ],
    bootstrap: [ AppComponent ],
    declarations: [
        AppComponent,
        SpaEmailValidator,
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
        WindowBrowser
    ]
})
export class AppModule {
}
