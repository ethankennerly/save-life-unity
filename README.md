# Demo to Replay a Session

This Unity demo replays a recorded session.

## Purpose

- A developer needs to collect steps to reproduce a bug.
- A user researcher needs to observe a live user session.

## Technical Requirements

This project is just a demo of one replay.

- Unity Editor replays a session.
- Replay of a button touches.
- Deterministic replay of a simple simulation that has random results.
- A sample recording source asset is less than 32 KB.
- A serialized sample recording could be compressed to less than 1 KB in a SQL database.
- Unity Test Runner in Edit Mode validates a replay in less than 1 second.
- Compatible with legacy input system.

## Architecture

### Touch Log

- Recorded random seed.
- Recorded touches compressed the session.

[Example Log](Assets/TouchLogs/RecordedTouches.asset)

### Interface Component System

ICS is analogous to Entity Component System (ECS).
Except a compile-time interface replaces a runtime entity.
A compile-time interface simplified maintenance.

- [C# Scripts](Assets/Scripts/Testables)
- [Tests](Assets/Editor/Tests)

## Installation

- [Project Version](ProjectSettings/ProjectVersion.txt)
