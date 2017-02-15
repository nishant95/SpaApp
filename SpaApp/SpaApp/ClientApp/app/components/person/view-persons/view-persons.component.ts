import { Component } from '@angular/core';
import { PersonDto } from '../../../dtos';
import { PersonService } from '../../../services';
import { SecurityService } from '../../../services';
import { BrowserModule } from '@angular/platform-browser';

@Component({
    selector: 'person',
    template: require('./view-persons.component.html')
})
export class ViewPersonsComponent {
    constructor(private personService: PersonService, private securityService: SecurityService) { }

    persons: PersonDto[] = [];
    ngOnInit() {
        console.log('person:ngOnInit _securityService.AuthorizedCallback');

        if (typeof window !== 'undefined' && window.location.hash) {
            this.securityService.AuthorizedCallback();
        }
    }

    ngAfterViewInit() {
        this.personService.getPersons().subscribe(
            value => {
                this.persons = <PersonDto[]>value.json()
            },
            error => { }
        );
    }

    setSelected(index: number) {

    }
}
