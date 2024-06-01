import React, { Component } from 'react';
import { Collapse, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import './NavMenu.css'
import IspoLogo from '../public/IspoLogo.svg';

const NavMenu: React.FC = () => {
  const [collapsed, setCollapsed] = React.useState(true);

  const toggleNavbar = () => {
    setCollapsed(!collapsed);
  };

  return (
      <header>
        <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3" container light>
          <NavbarBrand href="/">
            <img src={IspoLogo} alt="ISPO Logo" className="logo" />
          </NavbarBrand>
          <NavbarToggler onClick={toggleNavbar} className="mr-2" />
          <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={!collapsed} navbar>
            <ul className="navbar-nav flex-grow">
              <NavItem>
                <NavLink tag={Link} className="text-dark" to="/login">
                  Войти
                </NavLink>
              </NavItem>
            </ul>
          </Collapse>
        </Navbar>
      </header>
  );
};

export default NavMenu;
