// eslint-disable-next-line no-unused-vars
/* eslint-disable react/prop-types*/
import React, { useState, useRef } from "react";
import LogoImg from "./Logo";
import { useNavigate } from "react-router-dom";
import Button from "./Button";
import homePage from "../constants/homePageContent";
import LogOutButton from "./LogOutButton";
import { BG_STYLES, BUTTON_STYLES } from "../constants/tailwindStyles";
import { Icon, IconButton } from "@mui/material";
import { useDarkMode } from "../hooks/useDarkMode";
import { LightModeOutlined, DarkModeOutlined } from "@mui/icons-material";

const Nav = ({ navBtnRef }) => {
  const navigate = useNavigate();
  const { isDarkMode, toggleDarkMode } = useDarkMode();
  //const navBtnRef = useRef();

  let selectedBtnId;

  const selectNavBtn = (index) => {
    const selectedBtn = navBtnRef.current.childNodes[index - 1];

    if (isDarkMode) {
      selectedBtn.style.background = "#006DA4";
      selectedBtn.style.color = "#FFFFFF";
    } else {
      selectedBtn.style.background = "#19B9CE";
      selectedBtn.style.color = "#FFFFFF";
    }

    for (let i = 0; i < navBtnRef.current.childNodes.length; i++) {
      if (i !== index - 1) {
        const notSelectedBtn = navBtnRef.current.childNodes[i];
        if (isDarkMode) {
          notSelectedBtn.style.background = "";
          notSelectedBtn.style.color = "#FFFFFF";
        } else {
          notSelectedBtn.style.background = "";
          notSelectedBtn.style.color = "#0B5A64";
        }
      }
    }
  };

  const resetNavBtn = () => {
    for (let index = 0; index < navBtnRef.current.childNodes.length; index++) {
      const resetBtns = navBtnRef.current.childNodes[index];
      resetBtns.style.background = "";
      resetBtns.style.color = "#0B5A64";
    }
  };

  const getClickedBtn = () => {
    let clickedBtnId = 0;

    for (let index = 0; index < navBtnRef.current.childNodes.length; index++) {
      const element = navBtnRef.current.childNodes[index];
      if (
        element.style.background === "rgb(0, 109, 164)" ||
        element.style.background === "rgb(25, 185, 206)"
      ) {
        clickedBtnId = index + 1;
      }
    }
    return clickedBtnId;
  };

  const toggleDarkNavBtns = (index) => {
    const selectedBtn = navBtnRef.current.childNodes[index - 1];

    if (!isDarkMode) {
      selectedBtn.style.background = "#006DA4";
      selectedBtn.style.color = "#FFFFFF";
    } else {
      selectedBtn.style.background = "#19B9CE";
      selectedBtn.style.color = "#FFFFFF";
    }

    for (let i = 0; i < navBtnRef.current.childNodes.length; i++) {
      if (i !== index - 1) {
        const notSelectedBtn = navBtnRef.current.childNodes[i];
        if (!isDarkMode) {
          notSelectedBtn.style.background = "";
          notSelectedBtn.style.color = "#FFFFFF";
        } else {
          notSelectedBtn.style.background = "";
          notSelectedBtn.style.color = "#0B5A64";
        }
      }
    }
  };

  return (
    <div className={isDarkMode ? "dark" : ""}>
      <div
        className={`${BG_STYLES.NAV_BG} w-full fixed flex flex-wrap items-center justify-between mx-auto`}
      >
        <div className="pt-2 pl-6">
          <Button
            onClick={() => {
              resetNavBtn();
              navigate("/home");
            }}
          >
            <img
              src={
                isDarkMode
                  ? "../src/assets/Media/DARK_FM_noBG.png"
                  : "../src/assets/Media/FM_noBG.png"
              }
              alt="logo"
              className="w-[151px] h-[60px]"
            />
          </Button>
        </div>
        <div className="flex flex-wrap" ref={navBtnRef}>
          {homePage.map((h) => {
            return (
              <Button
                key={h.title}
                onClick={() => {
                  navigate(`/items/${h.id}`);
                  selectedBtnId = h.id;
                  selectNavBtn(selectedBtnId);
                }}
                className={`${BUTTON_STYLES.NAV_BUTTONS} py-2 pl-3 pr-4`}
              >
                {h.title}
              </Button>
            );
          })}
        </div>
        <div>
          <div className="px-6 flex flex-wrap">
            <div className="mr-4">
              <IconButton
                onClick={() => {
                  toggleDarkMode();
                  selectedBtnId = getClickedBtn();
                  toggleDarkNavBtns(selectedBtnId);
                }}
              >
                {isDarkMode ? (
                  <LightModeOutlined style={{ fill: "white" }} />
                ) : (
                  <DarkModeOutlined style={{ fill: "#0B5A64" }} />
                )}
              </IconButton>
            </div>
            <Button
              className={`${BUTTON_STYLES.NAV_ADMINBUTTON} px-4 py-2`}
              onClick={() => {
                navigate("/admin");
              }}
            >
              Administratie
            </Button>
            <div className="pl-4">
              <LogOutButton />
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Nav;
