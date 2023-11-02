import LogoImg from "./Logo";

const Nav = () => {
  return (
    <div className="bg-white flex flex-row justify-center w-full">
      <div className="bg-white w-[100%] h-[100px] relative">
        <div className="absolute w-[1005px] h-[33px] top-[58px] left-[305px]">
          <div className="absolute w-[152px] h-[33px] top-0 left-[849px]">
            <button
              href="#"
              className="hover:cursor-pointer relative w-[150px] h-[33px] bg-[#19b9ce] rounded-[10px] border-none"
            >
              <div className="absolute top-[7px] left-[23px] [font-family:'Inter-SemiBold',Helvetica] font-semibold text-white text-[16px] text-center tracking-[0] leading-[normal] whitespace-nowrap">
                Administratie
              </div>
            </button>
          </div>
          <div className="absolute w-[423px] h-[19px] top-[7px] left-0">
            <a
              href="#"
              className="no-underline absolute w-[166px] top-0 left-[257px] [font-family:'Inter-SemiBold',Helvetica] font-semibold text-black text-[16px] text-center tracking-[0] leading-[normal]"
            >
              Tankkaarten
            </a>
            <a
              href="#"
              className="no-underline absolute w-[162px] top-0 left-[129px] [font-family:'Inter-SemiBold',Helvetica] font-semibold text-black text-[16px] text-center tracking-[0] leading-[normal]"
            >
              Bestuurders
            </a>
            <a
              href="#"
              className="no-underline absolute w-[147px] top-0 left-0 [font-family:'Inter-SemiBold',Helvetica] font-semibold text-black text-[16px] text-center tracking-[0] leading-[normal]"
            >
              Voertuigen
            </a>
          </div>
        </div>
        <a href="#">
          <LogoImg />
        </a>
      </div>
    </div>
  );
};

export default Nav;
