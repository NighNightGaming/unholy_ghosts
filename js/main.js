var game = new Phaser.Game(800,600, Phaser.AUTO, '', { preload: preload, create: 
  create, update: update });

function preload() {
  game.load.image('sky', 'assets/sky.png');

  player = new Player(game);
  player.preload();

  enemy1 = new Mourner(game);
  enemy1.preload();
  game.load.image('mound', 'assets/mound.png');
  game.load.image('coffin', 'assets/coffin.png');
  game.load.image('platform', 'assets/platform.png');
  game.load.image('ground', 'assets/ground.png');
  console.log("Notice: Assets loaded");
}

function create() {
  //world creation
  game.add.sprite(0,0, 'sky');
  game.add.sprite(0,500, 'ground');
  game.add.sprite(400,400, 'platform').anchor.setTo(0.5,0.5);
  game.add.sprite(410,425, 'coffin').anchor.setTo(0.5,0.5);
  console.log("Notice: World created");

  mounds = game.add.group();
  for (var i = 0; i < 5; i += 1) {
    var mound = mounds.create(game.rnd.integerInRange(25, 700), 480, 'mound');
    mound.anchor.setTo(0.5,0.5);
  }
  console.log("Notice: Mounds created");

  //player creation
  player.create();
  console.log("Notice: Player created");

    //enemy creation 

  enemy1.create("female");
  console.log("Notice: Mourners created");
}

function update() {
  player.update();
  enemy1.update(player);
}