import logo from '../assets/Media/FM_noBG.png'

const LogoImg = () => {
    return (
        <div className="absolute w-[151px] h-[123px] top-[13px] left-[139px] object-cover">
            <img
                alt="Fleet removebg"
                src={logo}
            />
        </div>
    );
}

export default LogoImg;