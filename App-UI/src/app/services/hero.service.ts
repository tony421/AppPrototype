import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { delay, finalize } from 'rxjs/operators';
import { Hero, heroes } from '../models/hero';
import { LoadingService } from './loading.service';

@Injectable()
export class HeroService {
    constructor(private loadingService: LoadingService) { }

    delayMs = 500;

    // Fake server get; assume nothing can go wrong
    getHeroes(): Observable<Hero[]> {
        this.loadingService.show();
        return of(heroes).pipe(
            delay(this.delayMs),
            finalize(() => this.loadingService.hide())
        ); // simulate latency with delay
    }

    // Fake server update; assume nothing can go wrong
    updateHero(hero: Hero): Observable<Hero> {
        const oldHero = heroes.find(h => h.id === hero.id);
        const newHero = Object.assign(oldHero, hero); // Demo: mutate cached hero

        this.loadingService.show();
        return of(newHero).pipe(
            delay(this.delayMs),
            finalize(() => this.loadingService.hide())
        ); // simulate latency with delay
    }
}