import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { finalize } from 'rxjs/operators';
import { Hero } from 'src/app/models/hero';
import { HeroService } from 'src/app/services/hero.service';

@Component({
    selector: 'app-hero-list',
    templateUrl: './hero-list.component.html',
    styleUrls: ['./hero-list.component.scss']
})
export class HeroListComponent implements OnInit {
    heroes: Observable<Hero[]>;
    isLoading = false;
    selectedHero: Hero;
    message: string;

    constructor(private heroService: HeroService) { }

    ngOnInit() { this.getHeroes(); }

    onSucceeded($event) {
        if ($event) {
            this.message = this.selectedHero.name + ' got updated.';
            this.selectedHero = undefined;
        }
        else {
            this.message = 'Cancelled updating ' + this.selectedHero.name;
            this.selectedHero = undefined;
        }
    }

    getHeroes() {
        this.isLoading = true;
        this.heroes = this.heroService.getHeroes().pipe(finalize(() => this.isLoading = false));
        this.selectedHero = undefined;
    }

    select(hero: Hero) { 
        console.info("Selecing", hero);
        this.selectedHero = hero; 
    }
}
