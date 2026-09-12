# CLAUDE.md

Руководство для работы с кодом в этом репозитории. Здесь описаны реальные соглашения проекта, найденные в существующем коде — следуй им, даже если они отличаются от "стандартных" рекомендаций по C#/Unity, чтобы новый код не выбивался из стиля проекта.

## О проекте

Planet Adventure — Unity-проект (Unity **6000.0.32f1**, HDRP render pipeline).

## Структура папок

Весь игровой код находится в `Assets/Project/Scripts`. Assembly definition (.asmdef) файлов в проекте нет — весь код компилируется в `Assembly-CSharp`. Новый код добавляй в существующую структуру папок ниже, не создавай .asmdef без явного запроса.

```
Assets/Project/Scripts/
├── Agents/                    # Персонажи и их поведение
│   ├── AgentStateMachine/      # Стейт-машина агентов (AgentStates)
│   ├── AgentStrategy/          # Стратегии поведения агентов
│   ├── AgentView/               # Визуал/анимация (AgentIK)
│   └── PlayerAgent/            # Игрок: инпут, мувер, руки (Hand), стратегия поворота
├── AudioController/            # Аудиосистема (Ambient и т.д.)
├── CameraController/           # Управление камерой
├── DialogueSystem/             # Диалоги (Phrase, Speaker)
├── EntryPoint/                 # Точки входа сцен (GameEntryPoint, GameplayEntryPoint, MainMenuEntryPoint)
├── Environment/
│   ├── InteractiveObjects/     # Интерактивные объекты (Doors и т.д.)
│   └── QuestSystem/             # Система квестов
├── InputSystem/                # Обёртка над Unity Input System
├── Installers/                 # Zenject-инсталлеры (см. раздел Zenject)
├── PauseController/            # Пауза
├── SceneLoader/                 # Загрузка сцен
├── StorageService/              # Сохранение/загрузка (Serializable, Surrogates)
└── UI/
    ├── AuxiliaryUI/
    ├── GameplayUI/
    ├── MainMenuUI/
    ├── PopUpUI/
    ├── RootUI/SettingsMenuUI/
    └── SubtitlesUI/
```

Верхнеуровневые папки `Assets/Project/`: `Animations`, `Data`, `Materials`, `Meshes`, `PhysicsMaterials`, `Prefabs`, `Shaders`, `Sounds`, `Sprites`, `Textures`, `Videos`.

Прочее в `Assets/`: `Plugins/Zenject`, `Plugins/Demigiant` (DoTween), `Resources/`, `Settings/`, `TextMesh Pro/`.

## Кодстайл

- **Namespace не используются вообще** — все классы в глобальном namespace. Это осознанное соглашение проекта — не добавляй `namespace` в новые файлы без явного запроса.
- Отступ — **4 пробела**, без табов.
- Приватные поля, включая `[SerializeField]`, именуются `_camelCase`:
  ```csharp
  [SerializeField] private TMP_Text _indicator;
  private InputSystem _input;
  ```
- Поля группируются через `[Header("...")]`.
- Публичные свойства — PascalCase, auto-property с `private set`:
  ```csharp
  public bool IsPause { get; private set; }
  ```
- Простые однострочные методы пишутся как expression-bodied члены:
  ```csharp
  private void OnEnable() => _input.Player.Interact.performed += Interaction;
  ```
- `#region` не используются — не добавляй их.

## Инструменты

### Zenject

Используется активно для DI. MonoBehaviour-компоненты внедряют зависимости через **method injection** (не через конструктор, т.к. это MonoBehaviour):

```csharp
[Inject]
private void Construct(InputSystem input) => _input = input;
```

Такой паттерн — стандарт для всего проекта (см. `Agents/PlayerAgent/Hand.cs`, `CameraController/CameraController.cs`).

Биндинги настраиваются в инсталлерах в `Assets/Project/Scripts/Installers/`:
- `ProjectInstaller.cs` — общие для всего проекта биндинги.
- `GameplayInstaller.cs` — биндинги геймплейной сцены.

Оба — `MonoInstaller`, используют `Container.Bind<T>()...AsSingle()` и `Container.InstantiatePrefabForComponent<T>()`. Новые сервисы/системы регистрируй в подходящем из этих двух инсталлеров.

### DoTween (DG.Tweening)

Используется для простых анимаций UI и объектов сцены. В проекте применяются только одиночные твины без `DOTween.Sequence()` и без явного `.SetEase()` (используется ease по умолчанию):

```csharp
using DG.Tweening;

canvasGroup.DOFade(1f, duration);
transform.DOMove(targetPosition, duration);
image.DOScaleX(1f, duration);
```

Также встречаются `.DOPause()`, `.DOPlay()`, `.DOKill()` для управления твинами (см. `UI/GameplayUI/WarningIndicator.cs`). Придерживайся этого же простого стиля — если нужна цепочка анимаций, уточни у пользователя, стоит ли вводить `Sequence` (в проекте такого паттерна пока нет).

### Input System

Unity Input System обёрнут в кастомный сгенерированный класс `InputSystem` (совпадает по имени с `UnityEngine.InputSystem` — не путать). Внедряется через Zenject и используется так:

```csharp
_input.Player.Interact.performed += Interaction;
_input.UI.Cancel.performed += OnCancel;
```

### Прочее

- **TextMeshPro (TMPro)** — используется в UI-скриптах.
- **R3, Cinemachine, Addressables** — подключены как зависимости в `Packages/manifest.json`, но **не используются нигде в коде** на данный момент. Если задача требует одной из этих технологий — уточни у пользователя, прежде чем вводить новый паттерн использования, т.к. устоявшегося стиля для них в проекте ещё нет.

## Примечание

При добавлении нового кода в первую очередь ищи аналогичный существующий паттерн в соседних файлах той же папки и следуй ему — это не "рекомендации", а фактически принятый в проекте стиль.
