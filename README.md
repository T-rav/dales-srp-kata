# Single Responsibility Principle

*A class should have only one reason to change*

## Overview of this repo

Inside you will find the beginnings of a ground-breaking new RPG game. Unfortunately in the rush to get features completed the main
RpgPlayer class is starting to turn into a hair-ball of responsibilities.

## Getting started

1. Clone.
2. Build.
3. Run the tests.

## Assessing the current solution

Even though the public interface for the RpgPlayer looks nice and tidy, the nightmare exists behind the scenes.

The RpgPlayer has far too much knowledge about what the items are capable of, though initially this may have seemed like a good place to
put the logic as the items are *mostly* acting on the player.
