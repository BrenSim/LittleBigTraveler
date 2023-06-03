var data = [
    {
        img: "../imagesIndex/Pamukkale.png",
        country: "Denizli - Turquie",
        place: "Chateau Pamukkale",
        describe:
            "Une merveille naturelle et historique, avec ses cascades de terrasses en calcaire blanc, ses piscines thermales et ses ruines antiques, offrant une expérience unique de beauté et de détente.",
    },
    {
        img: "../imagesIndex/PragserWildsee.png",
        country: "Sud de Tyrol - Italie",
        place: "Lac de Braies",
        describe:
            "Un paradis alpin pittoresque, où vous pouvez profiter de paysages majestueux de montagnes enneigées, de charmants villages alpins et de la délicieuse cuisine tyrolienne.",
    },
    {
        img: "../imagesIndex/Algarve.png",
        country: "Algarve - Portugal",
        place: "Grotte Benagil",
        describe:
            "Un trésor caché, offrant une expérience immersive de stalactites et de stalagmites spectaculaires, de formations rocheuses uniques et d'une atmosphère mystique qui fascine les visiteurs.",
    },
    {
        img: "../imagesIndex/Geneve.png",
        country: "Geneve - Suisse",
        place: "Le Léman",
        describe:
            "Un véritable enchantement, avec ses eaux scintillantes qui s'étendent à perte de vue, entourées de sommets alpins, de charmants villages côtiers et de vignobles pittoresques, offrant une harmonie parfaite entre nature et culture.",
    },
    {
        img: "../imagesIndex/Paris.png",
        country: "Paris - France",
        place: "La tour Eiffel",
        describe:
            "L'emblématique monument parisien vous offre une vue spectaculaire sur la ville des lumières, un symbole incontournable de l'élégance et du romantisme français.",
    },
    {
        img: "../imagesIndex/GlowwormCaves.png",
        country: "L'ile du Nord - Islande",
        place: "Caves de Glowo",
        describe:
            "Explorez ces mystérieuses et éblouissantes caves, où des milliers de vers luisants illuminent les grottes souterraines, créant un spectacle enchanteur.",
    },
];

const introduce = document.querySelector(".introduce");
const ordinalNumber = document.querySelector(".ordinal-number");
ordinalNumber.innerHTML = "";
introduce.innerHTML = "";
for (let i = 0; i < data.length; i++) {
    introduce.innerHTML += `
        <div class="wrapper">
            <span>
                <h5 class="country" style="--idx: 0">${data[i].country}</h5>
            </span>  
            <span>
                <h1 class="place" style="--idx: 1">${data[i].place}</h1>
            </span>
            <span>
                <p class="describe" style="--idx: 2">${data[i].describe}</p>
            </span>
            <span>
                <button class="discover-button" style="--idx: 3">Venez découvrir</button>
            </span>
        </div>
    `;
    ordinalNumber.innerHTML += `<h2>0${i + 1}</h2>`;
}
introduce.children[0].classList.add("active");
ordinalNumber.children[0].classList.add("active");

const thumbnailListWrapper = document.querySelector(".thumbnail-list .wrapper");
thumbnailListWrapper.innerHTML += `<div class="thumbnail zoom"><img src="${data[0].img}" alt=""></div>`;
for (let i = 1; i < data.length; i++) {
    thumbnailListWrapper.innerHTML += `<div class="thumbnail" style="--idx: ${i - 1
        }"><img src="${data[i].img}" alt=""></div>`;
}

var currentIndex = 0;
const nextBtn = document.querySelector(".navigation .next-button");
nextBtn.addEventListener("click", () => {
    nextBtn.disabled = true;
    var clone = thumbnailListWrapper.children[0].cloneNode(true);
    clone.classList.remove("zoom");
    thumbnailListWrapper.appendChild(clone);
    thumbnailListWrapper.children[1].classList.add("zoom");
    setTimeout(() => {
        thumbnailListWrapper.children[0].remove();
        nextBtn.disabled = false;
    }, 1000);
    for (let i = 2; i < thumbnailListWrapper.childElementCount; i++) {
        thumbnailListWrapper.children[i].style = `--idx: ${i - 2}`;
    }
    if (currentIndex < data.length - 1) {
        currentIndex++;
    } else currentIndex = 0;
    for (let i = 0; i < data.length; i++) {
        introduce.children[i].classList.remove("active");
        ordinalNumber.children[i].classList.remove("active");
    }
    introduce.children[currentIndex].classList.add("active");
    ordinalNumber.children[currentIndex].classList.add("active");
    ordinalNumber.children[currentIndex].textContent = `0${currentIndex + 1}`;
});

