export class Hero {
    id = 0;
    name = '';
    power = '';
    sidekick = false;
    addresses: Address[];
  }
  
  export class Address {
    street = '';
    city   = '';
    state  = '';
    zip    = '';
  }
  
  export const heroes: Hero[] = [
    {
      id: 1,
      name: 'Whirlwind',
      power: 'flight',
      sidekick: true,
      addresses: [
        {street: '123 Main',  city: 'Anywhere', state: 'CA',  zip: '94801'},
        {street: '456 Maple', city: 'Somewhere', state: 'VA', zip: '23226'},
      ]
    },
    {
      id: 2,
      name: 'Bombastic',
      power: 'x-ray vision',
      sidekick: false,
      addresses: [
        {street: '789 Elm',  city: 'Smallville', state: 'OH',  zip: '04501'},
      ]
    },
    {
      id: 3,
      name: 'Magneta',
      power: 'strength',
      sidekick: false,
      addresses: [ ]
    },
  ];
  
  export const states = ['CA', 'MD', 'OH', 'VA'];