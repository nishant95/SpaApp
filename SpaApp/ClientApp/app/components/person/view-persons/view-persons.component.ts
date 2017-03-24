import { Component } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { Router } from "@angular/router";
import { PersonDto } from '../../../dtos';
import { PersonService, SecurityService } from '../../../services';

@Component({
    selector: 'person',
    template: require('./view-persons.component.html'),
    styles: [require('./view-persons.component.css')]
})
export class ViewPersonsComponent {
    constructor(private router: Router,
        private personService: PersonService,
        private securityService: SecurityService) { }

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

    editPerson(id: number) {
        var personId = this.persons[id].Id;
        if (this.securityService.HasAdminRole) this.router.navigateByUrl('person/edit/' + personId);
    }

    public Login() {
        console.log('Do login logic');
        this.securityService.Authorize();
    }
}
