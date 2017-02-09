import { Component } from '@angular/core';
import { PersonDto } from '../../../dtos/person.dto';
import { PersonService } from '../../../services/person.service';
import { BrowserModule } from '@angular/platform-browser';

@Component({
    selector: 'person',
    template: require('./view-persons.component.html')
})
export class ViewPersonsComponent {
    constructor(private personService: PersonService) {
    };

    persons: PersonDto[] = [];
    
    ngOnInit() {
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
